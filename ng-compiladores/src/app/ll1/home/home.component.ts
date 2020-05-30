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
    '3': `S -> A | B\nA -> a A S | B D\nB -> b B | f A C | %\nC -> c C | B D\nD -> g D | C | %`,
    '4': `exp -> term exp'\nexp' -> soma term exp' | %\nsoma -> + | -\nterm -> fator term'\nterm' -> mult fator term' | %\nmult -> *\nfator -> ( exp ) | num`
  }

  gramatica: any;

  constructor(private conversor: ConversorService,
    private primeiroService: PrimeiroService,
    private ll1Service: Ll1Service) { }

  ngOnInit(): void {
  }

  selecionar(event: any) {    
    this.entrada = this.exemplos[event.target.value];
  }

  processar() {
    this.erro = '';
    this.primeiros = [];
    this.seguintes = [];

    try {
      this.gramatica = this.ll1Service.construir(this.entrada);
      let variaveis = this.gramatica.variaveis;      

      variaveis.forEach(variavel => {
        let regra = this.gramatica.regras[variavel];
        this.primeiros.push({ variavel: variavel, valores: regra.first.join(',') });
        this.seguintes.push({ variavel: variavel, valores: regra.follow.join(',') });
      });
    }
    catch (error) {
      debugger;
      this.erro = error;
    }

  }

}
