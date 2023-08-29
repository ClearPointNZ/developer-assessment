import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { TodoItem } from '../models/todoitem';

@Injectable({ providedIn: 'root' })
export class TodoService {  
  static readonly ShowerUrl: string = 'api/todoitems';

  constructor (
    /** @internal */
    private http: HttpClient
  ) { }
  
  public fetch(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(TodoService.ShowerUrl);
  }
  
  public create(item: TodoItem) {
    return this.http.post<TodoItem>(TodoService.ShowerUrl, item);
  }
  
  public update(item: TodoItem) {
    let url = TodoService.ShowerUrl+'/'+item.id;

    return this.http.put<TodoItem>(url, item);
  }
}
