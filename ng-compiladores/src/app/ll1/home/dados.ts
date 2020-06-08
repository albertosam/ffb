import { Gramatica } from '../modelos/garmatica'
import { ValidadorLogger } from '../util/logger';

export const Combo: any[] = [
    { value: -1, text: 'Selecione' },
    { value: 1, text: 'Exemplo 1' },
    { value: 2, text: 'Exemplo 2' },
    { value: 3, text: 'Exemplo 3' },
    { value: 4, text: 'Exemplo 4' },
    { value: 5, text: 'Exemplo 5' }
]

export const GramaticasExemplo: any = {
    '1': `E -> T E’\nE’ -> + T E’ | %\nT -> F T’\nT’ -> * F T’ | %\nF -> ( E ) | id`,
    '2': `S -> A B d\nA -> a A | %\nB -> b B | c A | A C\nC -> c B | %`,
    '3': `S -> A | B\nA -> a A S | B D\nB -> b B | f A C | %\nC -> c C | B D\nD -> g D | C | %`,
    '4': `exp -> term exp'\nexp' -> soma term exp' | %\nsoma -> + | -\nterm -> fator term'\nterm' -> mult fator term' | %\nmult -> *\nfator -> ( exp ) | num`,
    '5': `S -> F | ( S + S )\nF -> a`
}

export const CodigosExemplo: any = {
    '4': `( num - num ) * num`,
    '5': `( ( a + a ) + a )`
}

export function toFirstView(gramatica: Gramatica) {
    let result = [];
    gramatica.variaveis.forEach(variavel => {
        let regra = gramatica.regras[variavel];
        result.push({ variavel: variavel, valores: regra.first.join(',') });
    });

    return result;
}

export function toFollowView(gramatica: Gramatica) {
    let result = [];
    gramatica.variaveis.forEach(variavel => {
        let regra = gramatica.regras[variavel];
        result.push({ variavel: variavel, valores: regra.follow.join(',') });
    });

    return result;
}

export function toLoggerTable(logger: ValidadorLogger) {
    let tabela = [[]];

    for (let index = 0; index < logger.logs.length; index++) {
        tabela[index] = [];
        tabela[index][0] = logger.logs[index].cadeia;
        tabela[index][1] = logger.logs[index].pilha.reverse().join('');
        tabela[index][2] = logger.logs[index].topo;
        tabela[index][3] = logger.logs[index].token;
        tabela[index][4] = logger.logs[index].mensagem;
        tabela[index][5] = logger.logs[index]?.erro;
        tabela[index][6] = logger.logs[index]?.sucesso;
    }

    return tabela;
}