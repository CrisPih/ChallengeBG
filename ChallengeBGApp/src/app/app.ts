import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MyToolBar } from './components/my-tool-bar/my-tool-bar';

@Component({
  imports: [RouterOutlet, MyToolBar],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App { }