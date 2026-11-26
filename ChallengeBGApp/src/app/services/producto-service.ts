import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
    CreateProductoRequest, GetProductoResponse, MensajeResponse, UpdateProductoRequest
} from '../interfaces/producto-dtos';

@Service()
export class ProductoService {

    private http = inject(HttpClient)
    private endPoint = `${environment.apiUrl}/producto`

    getAll(): Observable<GetProductoResponse[]> {
        return this.http.get<GetProductoResponse[]>(`${this.endPoint}/getAll`)
    }

    getById(id: number): Observable<GetProductoResponse> {
        return this.http.get<GetProductoResponse>(`${this.endPoint}/getById?id=${id}`)
    }

    create(req: CreateProductoRequest): Observable<MensajeResponse> {
        return this.http.post<MensajeResponse>(`${this.endPoint}/create`, req)
    }

    update(req: UpdateProductoRequest): Observable<MensajeResponse> {
        return this.http.put<MensajeResponse>(`${this.endPoint}/update`, req)
    }

    delete(id: number): Observable<MensajeResponse> {
        return this.http.delete<MensajeResponse>(`${this.endPoint}/delete/${id}`)
    }
}