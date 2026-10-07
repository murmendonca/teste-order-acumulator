import { Component, signal } from '@angular/core';
import { OrderGenerator } from './pages/order-generator/order-generator';

@Component({
  selector: 'app-root',
  imports: [OrderGenerator],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('order-generator');
}
