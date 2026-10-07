import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { OrdemRequest, Resultado } from '../core/models/ordem.model';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrdemService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/ordem-accumulators`;

   enviar(ordem: OrdemRequest) {
    return this.http.post<Resultado>(this.url, ordem);
  }
}
