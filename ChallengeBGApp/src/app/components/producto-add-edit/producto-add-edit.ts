import { Component, inject, OnInit, signal } from '@angular/core';
import { MatDialogRef, MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { applyEach, form, required, FormField } from '@angular/forms/signals';
import { GetProveedorResponse } from '../../interfaces/proveedor-dtos';
import { GetProductoResponse, MensajeResponse, ProductoProveedorRequest } from '../../interfaces/producto-dtos';
import { ProveedorService } from '../../services/proveedor-service';
import { ProductoService } from '../../services/producto-service';
import { Observable } from 'rxjs';

const nuevoPrecio = () => ({ proveedorId: '', numeroLote: '', precio: '', stock: '' });

@Component({
  imports: [MatDialogModule, MatSelectModule, MatInputModule,
    MatFormFieldModule, MatButtonModule, MatIconModule, FormField],
  selector: 'app-producto-add-edit',
  styleUrl: './producto-add-edit.scss',
  templateUrl: './producto-add-edit.html',
})
export class ProductoAddEdit implements OnInit {

  protected productoModel = signal({
    nombre: '',
    descripcion: '',
    precios: [nuevoPrecio()]
  })

  protected productoForm = form(this.productoModel, (field) => {
    required(field.nombre);
    applyEach(field.precios, (precio) => {
      required(precio.proveedorId);
      required(precio.numeroLote);
      required(precio.precio);
      required(precio.stock);
    });
  })

  protected proveedores = signal<GetProveedorResponse[]>([])
  protected guardando = signal(false)

  private proveedorService = inject(ProveedorService)
  private productoService = inject(ProductoService)
  private snackBar = inject(MatSnackBar)
  private dialogRef = inject(MatDialogRef<ProductoAddEdit>)

  protected data = inject<{ productoId?: number }>(MAT_DIALOG_DATA, { optional: true });
  protected isEditMode = signal<boolean>(false)

  ngOnInit(): void {
    this.proveedorService.getAll().subscribe({
      next: resp => this.proveedores.set(resp),
      error: () => this.close()
    })

    if (this.data?.productoId) {
      this.isEditMode.set(true)

      this.productoService.getById(this.data.productoId).subscribe({
        next: producto => this.loadProducto(producto),
        error: () => this.close()
      })
    }
  }

  private loadProducto(producto: GetProductoResponse): void {
    this.productoModel.set({
      nombre: producto.nombre,
      descripcion: producto.descripcion ?? '',
      precios: producto.precios.map(p => ({
        proveedorId: p.proveedorId.toString(),
        numeroLote: p.numeroLote,
        precio: p.precio.toString(),
        stock: p.stock.toString()
      }))
    })
  }

  addPrecio(): void {
    this.productoModel.update(m => ({ ...m, precios: [...m.precios, nuevoPrecio()] }))
  }

  removePrecio(index: number): void {
    this.productoModel.update(m => ({ ...m, precios: m.precios.filter((_, i) => i !== index) }))
  }

  save(): void {
    const { nombre, descripcion, precios } = this.productoForm().value()

    const preciosRequest: ProductoProveedorRequest[] = precios.map(p => ({
      proveedorId: Number(p.proveedorId),
      numeroLote: p.numeroLote,
      precio: Number(p.precio),
      stock: Number(p.stock)
    }))

    const request = { nombre, descripcion: descripcion || null, precios: preciosRequest }

    const peticion: Observable<MensajeResponse> = this.isEditMode()
      ? this.productoService.update({ id: this.data!.productoId!, ...request })
      : this.productoService.create(request)

    this.guardando.set(true)

    peticion.subscribe({
      next: resp => {
        this.snackBar.open(resp.mensaje, 'Cerrar', { duration: 3000 })
        this.dialogRef.close(true)
      },
      error: () => this.guardando.set(false)
    })
  }

  close(): void {
    this.dialogRef.close(false)
  }
}