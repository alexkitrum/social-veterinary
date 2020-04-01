import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {Observable, of} from 'rxjs';
import {IPet} from 'src/app/shared/interfaces/pet.model';
import {catchError, tap} from 'rxjs/operators';
import {ToastrService} from './toastr.service';
import {HttpClientHelper} from './client-helper';

@Injectable()
export class PetsService {

  constructor(private http: HttpClient, private toastr: ToastrService) {

  }

  add(personId, pet): Observable<IPet> {
    return this.http.put<IPet>(`${HttpClientHelper.baseURL}/api/persons/${personId}/pets`, pet, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
      .pipe(catchError(this.handlerError<IPet>('add', null)))
      .pipe(tap((item: IPet) => {
        return item;
      }));
  }

  getPetsForPerson(personId): Observable<IPet[]> {
    return this.http.get<IPet[]>(`${HttpClientHelper.baseURL}/api/persons/${personId}/pets`)
      .pipe(catchError(this.handlerError<IPet[]>('getPetsForPerson', [])));
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
