import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {ResultItem} from '../models/result.item.model';

@Injectable()
export class PetsService{
    private baseUri: string;
    constructor(private http: HttpClient ) { this.baseUri = "https://localhost:5001/"; }

    ngOnInit(){
    }

    getPetsData(petType:string){
        var headers = new HttpHeaders().set('person-pets-api-key', 'fahsafh6as09fajoafuasdjdjo@jasdafdsfyo;hadfauiadfua;do=[fasdgsfvbfgsfdgstfs');
        return this.http.get<ResultItem[]>(this.baseUri + 'PersonPets?petType=' + petType, { 'headers': headers });
    }
}