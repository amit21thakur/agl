import { Input, Component, OnInit } from '@angular/core';
import { ResultItem } from 'src/app/models/result.item.model';

@Component({
  selector: 'app-pets',
  templateUrl: './pets.component.html',
  styleUrls: ['./pets.component.css']
})
export class PetsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  private _pets:ResultItem;
  get pets() {
    return this._pets;
  }
  @Input()
  set pets(pets:ResultItem) {
    this._pets = pets;
  }
  

}
