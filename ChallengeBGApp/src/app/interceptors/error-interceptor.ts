import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth-service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.error(`Error ${error.status} en ${req.method} ${req.url}`, error);

      const sesionVencida = error.status === 401 && !req.url.endsWith('/auth/login');
      if (sesionVencida) authService.logout();

      snackBar.open(getMensaje(error, sesionVencida), 'Cerrar', { duration: 5000 });

      return throwError(() => error);
    })
  );
};

function getMensaje(error: HttpErrorResponse, sesionVencida: boolean): string {
  if (error.status === 0) return 'No se pudo conectar con el servidor';
  if (sesionVencida) return 'Su sesión expiró, ingrese nuevamente';

  return error.error?.mensaje ?? 'Ocurrió un error inesperado, intente nuevamente';
}