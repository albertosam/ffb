import { Component, OnInit } from '@angular/core';
import { ConversorService } from '../services/conversor.service';
import { PrimeiroService } from '../services/primeiro.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  entrada: string;

  constructor(private conversor: ConversorService, private primeiroService: PrimeiroService) { }

  ngOnInit(): void {
  }

  processar() {
    debugger;
    let regras = this.conversor.converter(this.entrada);
    let a = this.primeiroService.primeiro(regras);
  }

}
