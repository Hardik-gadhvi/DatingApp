import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {

  baseUrl = "https://localhost:5001/api/";
  validationErrors: string[] = [];

  constructor(private http:HttpClient){}

  get404error(){
    this.http.get(this.baseUrl +'error/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400error(){
    this.http.get(this.baseUrl +'error/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get500error(){
    this.http.get(this.baseUrl +'error/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get401error(){
    this.http.get(this.baseUrl +'error/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400ValidationError(){
    this.http.get(this.baseUrl +'account/register', {}).subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error);
        this.validationErrors = error;
      }
    })
  }
}
