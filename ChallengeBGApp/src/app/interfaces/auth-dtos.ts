export interface LoginRequest {
    nombreUsuario: string;
    clave: string;
}

export interface LoginResponse {
    token: string;
    nombreCompleto: string;
    expira: string;
}
