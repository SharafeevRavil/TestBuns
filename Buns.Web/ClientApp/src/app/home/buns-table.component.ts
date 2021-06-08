import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';
@Component({
  selector: 'app-buns-table',
  templateUrl: './buns-table.component.html'
})
export class BunsTableComponent {
  public buns: Bun[];
  moment: any = moment;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Bun[]>(baseUrl + 'bun').subscribe(result => {
      this.buns = result;
    }, error => console.error(error));
  }
}

interface Bun {
  id: number,
  typeName: string;
  startPrice: number;
  currentPrice: number;
  nextPrice: number;
  nextPriceChangeTime: Date;
}
