import { FormsModule } from '@angular/forms';
import { waitForAsync, TestBed, ComponentFixture } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing'
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { of } from 'rxjs/internal/observable/of';

import { TodoItem } from "../../models/todoitem";
import { TodoService } from "../../service/todo.service";
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let todoService: jasmine.SpyObj<TodoService>;
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(waitForAsync(() => {
    todoService = jasmine.createSpyObj("TodoService", ["fetch", "create", "update"]); 
        
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        FormsModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        { provide: TodoService, useValue: todoService}
      ]
    }).compileComponents();
    
  }));

  beforeEach(() => {
    const response: TodoItem[] = [
      { id: '1', description: 'A', isCompleted: false },
      { id: '2', description: 'A', isCompleted: false }
    ];
    todoService.fetch.and.returnValue(of(response));
    todoService.create.and.returnValue(of(response[0]));
    todoService.update.and.returnValue(of());

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {   
    expect(component).toBeTruthy();
    expect(component.items.length).toBe(2);
  });

  it('should create todo item', () => {
    const inputEl = fixture.debugElement.query(By.css('#formAddTodoItem'));
    inputEl.nativeElement.value = 'A';
    inputEl.nativeElement.dispatchEvent(new Event('input'));
    
    const buttonEl = fixture.debugElement.query(By.css('#add_button'));
    buttonEl.triggerEventHandler('click', null);

    expect(component.description).toBe('A');
    expect(todoService.create).toHaveBeenCalled();
  });

});