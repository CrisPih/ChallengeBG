import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { form, required, FormField } from '@angular/forms/signals';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth-service';

@Component({
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, FormField],
  selector: 'app-login',
  styleUrl: './login.scss',
  templateUrl: './login.html',
})
export class Login {

  private authService = inject(AuthService)
  private router = inject(Router)

  protected loginModel = signal({ nombreUsuario: '', clave: '' })

  protected loginForm = form(this.loginModel, (field) => {
    required(field.nombreUsuario);
    required(field.clave);
  })

  protected cargando = signal(false)

  login(event: Event): void {
    event.preventDefault()
    this.cargando.set(true)

    this.authService.login(this.loginForm().value()).subscribe({
      next: () => this.router.navigate(['/productos']),
      error: () => this.cargando.set(false)
    })
  }
}