
export interface Token {
    isVariavel: boolean;
    texto: string;
}

export interface GramaticaRegra {
    texto: string;
    partes: number[];
    first: string[];
    follow: string[];
}

export interface Gramatica {
    variaveis: string[];
    terminais: string[];
    regras: { [key: string]: GramaticaRegra };
    indices: Indice[]
    tamanho: number;
    tabela: any[];
}

export interface Indice {
    texto: string;
    tokens: Token[];
    variavel: string;
}
