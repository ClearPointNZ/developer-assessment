import { Component, OnInit } from '@angular/core';

import { TodoItem } from 'src/app/models/todoitem';
import { TodoService } from 'src/app/service/todo.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  items: TodoItem[] = [];
  description: string = '';

  itemInEditing: TodoItem = null;

  public constructor(
    /** @internal */
    private service: TodoService
  ) {}

  ngOnInit(): void {
    this.getItems();
  }

  getItems() {
    this.service.fetch().subscribe((response: TodoItem[]) => {
      if (response) {
        this.items = response;
      }
    });
  }

  handleAdd() {
    const item = {
      id: crypto.randomUUID(),
      description: this.description,
      isCompleted: false
    }

    this.service.create(item).subscribe(() => {
      this.getItems();
    });
  }

  handleClear() {
    this.description = '';
    this.itemInEditing = null;
  }

  handleMarkAsComplete(item: TodoItem) {
    item.isCompleted = true;

    this.service.update(item).subscribe(() => {
      this.getItems();
    });
  }

  handleEdit(item: TodoItem) {
    if (this.itemInEditing)
    {
      alert('You are already editing an item!');
    } else {
      this.itemInEditing = item;
      this.description = item.description;
    }
  }

  handleUpdate() {
    this.itemInEditing.description = this.description;

    this.service.update(this.itemInEditing).subscribe(() => {      
      this.handleClear();
      this.getItems();
    });
  }
}
