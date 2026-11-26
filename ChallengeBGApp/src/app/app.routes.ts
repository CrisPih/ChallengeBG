import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { ProductoList } from './components/producto-list/producto-list';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
    { path: 'login', component: Login },
    { path: 'productos', component: ProductoList, canActivate: [authGuard] },
    { path: '**', redirectTo: 'productos' }
];