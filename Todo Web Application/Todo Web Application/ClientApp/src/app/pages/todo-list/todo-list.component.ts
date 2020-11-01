import { Component, OnInit } from '@angular/core';
import { TodoService } from 'src/app/shared/services/todo.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  todos : any;
  addTaskForm: FormGroup;
    
  constructor(private fb: FormBuilder, private todoService: TodoService) {
    this.addTaskForm = this.fb.group({
      description: ['', Validators.required]
    });
  }

  async ngOnInit() {
    await this.getTodoList();
  }

  async addTask(){
    if (this.addTaskForm.invalid) {
      return;
    }

    await this.todoService.addTask(this.addTaskForm.value);
    await this.getTodoList();
    this.addTaskForm.controls['description'].setValue('');
  }

  async getTodoList(){
    this.todos = await this.todoService.getTodoList();
  }
  
  async toggleStatus(todoTransactionId){
    await this.todoService.toggleStatus(todoTransactionId);
    await this.getTodoList();
  }

  async deleteTodoTransaction(todoTransactionId){
    await this.todoService.deleteTodoTransaction(todoTransactionId);
    await this.getTodoList();
  }
  async clearCompletedTodoTransaction(){
    await this.todoService.clearCompletedTodoTransaction();
    await this.getTodoList();
  }
}
