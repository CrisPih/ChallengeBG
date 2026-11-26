import { HttpClient } from '@angular/common/http';
import { computed, inject, Service, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginRequest, LoginResponse } from '../interfaces/auth-dtos';

const SESION_KEY = 'sesion';

@Service()
export class AuthService {

    private http = inject(HttpClient)
    private router = inject(Router)
    private endPoint = `${environment.apiUrl}/auth`

    private sesion = signal<LoginResponse | null>(this.readSesion())

    readonly nombreCompleto = computed(() => this.sesion()?.nombreCompleto ?? '')

    login(req: LoginRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(`${this.endPoint}/login`, req)
            .pipe(tap(resp => this.saveSesion(resp)))
    }

    logout(): void {
        localStorage.removeItem(SESION_KEY)
        this.sesion.set(null)
        this.router.navigate(['/login'])
    }

    isLoggedIn(): boolean {
        const sesion = this.sesion()
        return sesion !== null && new Date(sesion.expira) > new Date()
    }

    getToken(): string | null {
        return this.sesion()?.token ?? null
    }

    private saveSesion(resp: LoginResponse): void {
        localStorage.setItem(SESION_KEY, JSON.stringify(resp))
        this.sesion.set(resp)
    }

    private readSesion(): LoginResponse | null {
        const guardada = localStorage.getItem(SESION_KEY)
        return guardada ? JSON.parse(guardada) : null
    }
}