import { incluirValores, incluirValor } from './listas';
import { getConjuntoFirst } from './first';
import { Gramatica } from '../modelos/garmatica';


/**
 * αƐß
 * 
 * 1) Se S é inicial, então Follow(S) = {$}. $ Indica final da sentença.
 * 2) A -> αXß , então Follow(X) = {Fisrt(ß) - Ɛ}
 * 3) A -> αX ou A -> αXß , onde First(ß) contém Ɛ então Follow(X) contém Follow(A)
 *    
 * 
 * @param variavel 
 * @param variavelInicial 
 * @param inicio 
 */
export function getConjuntoFollow(variavel: string, variavelInicial: string, inicio: boolean, gramatica: Gramatica, pilhaDeBusca: string[]) {
    let resultado: string[] = [];

    // se a variável que está buscando já está pilha de busca
    // evitar loop infinito
    if (pilhaDeBusca.includes(variavel))
        return resultado;

    // inclui variável na pilha de busca
    pilhaDeBusca.push(variavel);

    if (variavel == variavelInicial && !inicio)
        return resultado;

    // regra (1) - VARIAVEL é o não terminal inicial
    if (gramatica.indices[1].variavel == variavel)
        resultado = ['$'];

    for (let i = 1; i <= gramatica.tamanho; i++) {
        const atual = gramatica.indices[i];
        let encontrado = false;

        for (let j = 0; j < atual.tokens.length; j++) {
            const token = atual.tokens[j];

            // encontra variável em regra de produção
            if (token.texto === variavel) {
                encontrado = true;

                // regra (2) - o conjunto FIRST do token seguinte, é adicionado ao conjunto FOLLOW(VARIAVEL)
                if (j == atual.tokens.length - 1 && atual.variavel != token.texto) {
                    let seguinte = getConjuntoFollow(atual.variavel, variavelInicial, false, gramatica, pilhaDeBusca);
                    resultado = incluirValores(resultado, seguinte);
                }
                continue;
            }

            if (encontrado == true) {
                if (token.isVariavel == false) {
                    resultado = incluirValor(resultado, token.texto);
                    break;
                }

                let primeiro = getConjuntoFirst(token.texto, gramatica);
                // regra (3) - FIRST contém Ɛ
                if (primeiro.join('').endsWith('%')) {
                    if (j == atual.tokens.length - 1) {
                        let seguinte = getConjuntoFollow(atual.variavel, variavelInicial, false, gramatica, pilhaDeBusca);
                        resultado = incluirValores(resultado, seguinte);
                    }

                    primeiro.splice(primeiro.length - 1, 1);
                    resultado = incluirValores(resultado, primeiro);
                }
                else {
                    resultado = incluirValores(resultado, primeiro);
                    break;
                }
            }
        }
    }

    return resultado;
}