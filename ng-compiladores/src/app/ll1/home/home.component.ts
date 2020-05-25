import { Component, OnInit } from '@angular/core';
import { ConversorService } from '../services/conversor.service';
import { PrimeiroService } from '../services/primeiro.service';
import { Ll1Service } from '../services/ll1.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  erro: string = '';
  entrada: string = '';
  primeiros: { variavel: string, valores: string }[] = [];
  seguintes: { variavel: string, valores: string }[] = [];
  exemplos: any = {
    '1': `E -> T E’\nE’ -> + T E’ | %\nT -> F T’\nT’ -> * F T’ | %\nF -> ( E ) | id`,
    '2': `S -> A B d\nA -> a A | %\nB -> b B | c A | A C\nC -> c B | %`,
    '3': `S -> A | B\nA -> a A S | BD\nB -> b B | f A C | %\nC -> cC | B D\nD -> g D | C | %`
  }

  constructor(private conversor: ConversorService,
    private primeiroService: PrimeiroService,
    private ll1Service: Ll1Service) { }

  ngOnInit(): void {
  }

  selecionar(event: any) {
    debugger;
    this.entrada = this.exemplos[event.target.value];
  }

  processar() {
    this.erro = '';
    this.primeiros = [];

    try {
      let gramatica = this.ll1Service.construir(this.entrada);
      let variaveis = gramatica.variaveis;
      debugger;

      variaveis.forEach(variavel => {
        let regra = gramatica.regras[variavel];
        this.primeiros.push({ variavel: variavel, valores: regra.primeiros.join(',') });
        this.seguintes.push({ variavel: variavel, valores: regra.seguintes.join(',') });
      });
    }
    catch (error) {
      debugger;
      this.erro = error;
    }

  }

}
