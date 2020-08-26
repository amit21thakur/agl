import {Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PetsService{
    private baseUri: string;
    constructor(private http: HttpClient ) { this.baseUri = "https://localhost:5001/"; }

    ngOnInit(){
    }
    
    getPetsData(petType:string){
        return this.http.get<ResultItem[]>('PersonPets?petType=' + petType);
    }
}