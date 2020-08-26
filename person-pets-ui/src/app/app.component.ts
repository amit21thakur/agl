import { Component, OnInit } from '@angular/core';
import { PetsService } from './services/pets.service';
import { ResultItem } from './models/result.item.model';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [PetsService]
})
export class AppComponent implements OnInit {

  constructor(private petsService: PetsService)
  {
  }

  ngOnInit(): void {
    this.fetchData('cat');
  }

  fetchData(petType:string){
    this.petsService.getPetsData(petType).subscribe(
      (response) => {
        this.petsForGender = response;
      },
      (err) => {
        console.log(err)
        this.HasError = true},

    );
  }

  title = 'person-pets-ui';

  private _petsForGender:ResultItem[];
  public set petsForGender(data)
  {
    this._petsForGender = data;
    this.HasError = false;
  }
  public get petsForGender()
  {
    return this._petsForGender;
  }


  private _hasError;
  public get HasError()
  {
    return this._hasError;
  }
  public set HasError(hasError)
  {
    this._hasError = hasError;
  }


}
