import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  items: any[] = [];
  description: string = '';

  public constructor(private httpClient: HttpClient) {}

  getItems() {
    alert('todo');
  }

  handleAdd() {
    alert('todo');
  }

  handleClear() {
    this.description = '';
  }

  handleMarkAsComplete(item: any) {
    alert('todo');
  }
}
