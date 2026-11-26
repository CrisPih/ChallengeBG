import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { GetProveedorResponse } from '../interfaces/proveedor-dtos';

@Service()
export class ProveedorService {

    private http = inject(HttpClient)
    private endPoint = `${environment.apiUrl}/proveedor`

    getAll(): Observable<GetProveedorResponse[]> {
        return this.http.get<GetProveedorResponse[]>(`${this.endPoint}/getAll`)
    }
}