import { Component, OnInit } from '@angular/core';
import { Ll1Service } from '../services/ll1.service';
import { Ll1ParserService } from '../services/ll1-parser.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  erro: string = '';
  entrada: string = '';
  validacao: string;
  primeiros: { variavel: string, valores: string }[] = [];
  seguintes: { variavel: string, valores: string }[] = [];
  exemplos: any = {
    '1': `E -> T E’\nE’ -> + T E’ | %\nT -> F T’\nT’ -> * F T’ | %\nF -> ( E ) | id`,
    '2': `S -> A B d\nA -> a A | %\nB -> b B | c A | A C\nC -> c B | %`,
    '3': `S -> A | B\nA -> a A S | B D\nB -> b B | f A C | %\nC -> c C | B D\nD -> g D | C | %`,
    '4': `exp -> term exp'\nexp' -> soma term exp' | %\nsoma -> + | -\nterm -> fator term'\nterm' -> mult fator term' | %\nmult -> *\nfator -> ( exp ) | num`,
    '5': `S -> F | ( S + S )\nF -> a`
  }

  exemploValidacao: any = {
    '4': `( num - num ) * num`,
    '5': `( ( a + a ) + a )`
  }

  logger: any[][];

  gramatica: any;
  processado: boolean = false;

  constructor(private ll1Service: Ll1Service, private parser: Ll1ParserService) { }

  ngOnInit(): void {
  }

  selecionar(event: any) {
    this.entrada = this.exemplos[event.target.value];
    this.validacao = this.exemploValidacao[event.target.value];
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

      if (this.validacao) {
        this.parser.validate(this.validacao, this.gramatica);
        this.logger = this.parser.logger.toTable();
      }

      this.processado = true;
    }
    catch (error) {
      debugger;
      this.erro = error;
    }

  }

  validar() {
    debugger;

  }

}
