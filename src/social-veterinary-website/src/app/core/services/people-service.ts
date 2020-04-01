import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {Observable, of} from 'rxjs';
import {IPerson} from 'src/app/shared/interfaces/person.model';
import {catchError, tap} from 'rxjs/operators';
import {ToastrService} from './toastr.service';

@Injectable()
export class PeopleService {
  people: IPerson[] = [];

  constructor(private http: HttpClient, private toastr: ToastrService) {

  }

  getById(id: number): Observable<IPerson> {
    return this.http.get<IPerson>(`/api/persons/${id}`);
  }

  add(person): Observable<IPerson> {
    return this.http.put<IPerson>('/api/persons', person, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
      .pipe(catchError(this.handlerError<IPerson>('add', null)))
      .pipe(tap((item: IPerson) => {
        this.people.push(item);
      }));
  }

  getAll(): Observable<IPerson[]> {
    return this.http.get<IPerson[]>('/api/persons')
      .pipe(catchError(this.handlerError<IPerson[]>('getAll', [])));
  }

  private handlerError<T>(operation = 'operation', result?: T) {
    return (reponse: any): Observable<T> => {
      console.error(reponse);
      let errorMessage = reponse.error.Message;
      if (reponse.error.Errors) {
        errorMessage = reponse.error.Errors.map(x => x.Message).join('<br/>');
      }
      this.toastr.error(errorMessage);
      return of(result as T);
    };
  }
}
