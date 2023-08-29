import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule, 
  HttpTestingController} from '@angular/common/http/testing'

import { TodoItem } from "../models/todoitem";
import { TodoService } from "./todo.service";

describe('ToDoService', () => {
  let service: TodoService;
  let httpCtrl: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule]
    });
    
    service = TestBed.inject(TodoService);
    httpCtrl = TestBed.inject(HttpTestingController);
  });

  it('#fetch - should return real value', (done: DoneFn) => {
    const expected: TodoItem[] =
      [
        { id: '1', description: 'A', isCompleted: false },
        { id: '2', description: 'A', isCompleted: false }
      ];

    service.fetch().subscribe(value => {
      expect(value).toEqual(expected);
      done();
    });

    const mockHttp = httpCtrl.expectOne(TodoService.ShowerUrl);
    const httpRequest = mockHttp.request;

    expect(httpRequest.method).toEqual("GET");

    mockHttp.flush(expected);

    httpCtrl.verify();
  });

  it('#create - should success', (done: DoneFn) => {
    const value: TodoItem =
      { id: '1', description: 'A', isCompleted: false };

    service.create(value).subscribe((body) => {
      expect(body).toEqual(value);

      done();
    });

    const mockHttp = httpCtrl.expectOne(TodoService.ShowerUrl);
    const httpRequest = mockHttp.request;

    expect(httpRequest.method).toEqual("POST");

    mockHttp.flush(value, { status: 201, statusText: 'Created' });

    httpCtrl.verify();
  });

  it('#update - should success', (done: DoneFn) => {
    const value: TodoItem =
      { id: '1', description: 'A', isCompleted: false };

    service.update(value).subscribe(() => {      
      done();
    });

    const mockHttp = httpCtrl.expectOne(TodoService.ShowerUrl+'/'+value.id);
    const httpRequest = mockHttp.request;

    expect(httpRequest.method).toEqual("PUT");

    mockHttp.flush('', { status: 204, statusText: 'No Content' });

    httpCtrl.verify();
  });

});