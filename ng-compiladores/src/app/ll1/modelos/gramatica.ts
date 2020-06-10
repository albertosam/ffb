
/**
 * Unidade mínima da minha gramática
 */
export interface Token {
    isVariavel: boolean;
    texto: string;
}

/**
 * Conjunto de tokens que definem uma produção
 */
export interface GramaticaRegra {
    texto: string;
    partes: number[];
    first: string[];
    follow: string[];
}

/***
 * Determina índice de expressões
 */
export interface Indice {
    texto: string;
    tokens: Token[];
    variavel: string;
}

/**
 * Implementação da gramática 
 */
export class Gramatica {
    variaveis: string[];
    terminais: string[];
    regras: { [key: string]: GramaticaRegra };
    indices: Indice[]
    tamanho: number;
    tabela: any[];

    constructor() {
        this.variaveis = [];
        this.terminais = ['$'];
        this.regras = {};
        this.indices = [];
        this.tamanho = 0;
        this.tabela = [];
    }
}
