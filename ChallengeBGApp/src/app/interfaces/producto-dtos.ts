export interface ProductoProveedorRequest {
    proveedorId: number;
    numeroLote: string;
    precio: number;
    stock: number;
}

export interface CreateProductoRequest {
    nombre: string;
    descripcion: string | null;
    precios: ProductoProveedorRequest[];
}

export interface UpdateProductoRequest {
    id: number;
    nombre: string;
    descripcion: string | null;
    precios: ProductoProveedorRequest[];
}

export interface GetProductoProveedorResponse {
    proveedorId: number;
    proveedorNombre: string;
    numeroLote: string;
    precio: number;
    stock: number;
}

export interface GetProductoResponse {
    id: number;
    nombre: string;
    descripcion: string | null;
    precios: GetProductoProveedorResponse[];
}

export interface MensajeResponse {
    mensaje: string;
}