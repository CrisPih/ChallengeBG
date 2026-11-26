import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from '../../services/auth-service';

@Component({
  imports: [MatToolbarModule, MatButtonModule, MatIconModule],
  selector: 'app-my-tool-bar',
  styleUrl: './my-tool-bar.scss',
  templateUrl: './my-tool-bar.html',
})
export class MyToolBar {
  protected authService = inject(AuthService)
}