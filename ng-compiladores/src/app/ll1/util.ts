import { Regra } from './modelos/regra';


export const LAMBDA = 'LAMBDA';

export function getVariaveis(regras: Regra[]): string[] {
    return regras.map(a => a.variavel);
}

export function getTerminais(regras: Regra[]): string[] {
    let simbolos = [];
    let variavies = getVariaveis(regras);

    regras.forEach(a => {
        let sub = a.cadeias.reduce((x, y) => x.concat(y), []);
        simbolos = simbolos.concat(sub);
    });

    // elimina variáveis
    let terminais = simbolos.filter(a => variavies.indexOf(a) === -1);
    // elimina repetições
    terminais = terminais.filter((v, i, array) => array.indexOf(v) === i);
    return terminais;
}