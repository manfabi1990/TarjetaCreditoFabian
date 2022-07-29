import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TarjetaService {

  private myAppUrl: string = "https://localhost:44352/";
  private myApiUrl: string = "Api/Tarjeta/";

  constructor(private http: HttpClient) { }

  getListTarjetas(): Observable<any>{

    return this.http.get(this.myAppUrl + this.myApiUrl);

  }

  deleteTarjeta(numeroTarjeta: number): Observable<any>{

    debugger
    return this.http.delete(this.myAppUrl + this.myApiUrl + "?numeroTarjeta=" + numeroTarjeta.toString());

  }

  saveTarjeta(tarjeta: any): Observable<any>{


    return this.http.post(this.myAppUrl + this.myApiUrl, tarjeta);

  }

  editTarjeta(numeroTarjeta: string, tarjeta:any): Observable<any>{

    return this.http.put(this.myAppUrl + this.myApiUrl + "?numeroTarjeta=" + numeroTarjeta, tarjeta);


  }

}
