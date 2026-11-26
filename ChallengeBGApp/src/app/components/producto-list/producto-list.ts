import { Component, effect, inject, OnInit, signal, viewChild } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductoAddEdit } from '../producto-add-edit/producto-add-edit';
import { GetProductoResponse } from '../../interfaces/producto-dtos';
import { ProductoService } from '../../services/producto-service';
import Swal from 'sweetalert2';

@Component({
  imports: [MatButtonModule, MatIconModule, MatPaginatorModule, MatTableModule,
    MatInputModule, MatFormFieldModule, CurrencyPipe],
  selector: 'app-producto-list',
  styleUrl: './producto-list.scss',
  templateUrl: './producto-list.html',
})
export class ProductoList implements OnInit {

  readonly dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private productoService = inject(ProductoService);

  protected displayedColumns: string[] = ['nombre', 'descripcion', 'precios', 'actions'];
  protected dataSource = new MatTableDataSource<GetProductoResponse>([]);
  protected paginator = viewChild(MatPaginator);
  protected productos = signal<GetProductoResponse[]>([]);

  constructor() {
    effect(() => {
      this.dataSource.data = this.productos();

      if (this.paginator())
        this.dataSource.paginator = this.paginator()!;
    })
  }

  ngOnInit(): void {
    this.loadProductos();
  }

  private loadProductos(): void {
    this.productoService.getAll().subscribe({
      next: (data) => this.productos.set(data),
      error: () => this.productos.set([])
    })
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openModal(productoId?: number): void {
    const dialogRef = this.dialog.open(ProductoAddEdit, {
      width: '700px',
      maxWidth: '95vw',
      disableClose: true,
      data: { productoId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.loadProductos();
    })
  }

  deleteProducto(id: number): void {
    Swal.fire({
      title: '¿Desea eliminar el producto?',
      text: 'Esta acción no se puede deshacer',
      showCancelButton: true,
      confirmButtonColor: '#010a88',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (!result.isConfirmed) return;

      this.productoService.delete(id).subscribe({
        next: (resp) => {
          this.snackBar.open(resp.mensaje, 'Cerrar', { duration: 3000 });
          this.loadProductos();
        },
        error: () => this.loadProductos()
      })
    });
  }
}