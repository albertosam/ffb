import { Gramatica } from '../modelos/gramatica';
import { incluirValores, incluirValor } from './listas';

/**
 * αƐß
 * 
 * 0) First(Ɛ) = {Ɛ}
 * 1) Fisrt(a) = {a} onde a é terminal
 * 2) A -> aα , então First(A) = {a} sendo a terminal
 * 3) A -> Bα , então First(A) = First(B) , onde B não deriva em Ɛ
 * 4) A -> Bα , então First(A) = {First(B) - Ɛ} U {First(α)} , onde B deriva em Ɛ
 * 
 *  
 * 
 * @param variavel 
 */
export function getConjuntoFirst(variavel: string, gramatica: Gramatica): string[] {
    var resultado: string[] = [];

    gramatica.regras[variavel].partes.forEach(parte => {
        for (var i = 0; i < gramatica.indices[parte].tokens.length; i++) {
            let token = gramatica.indices[parte].tokens[i];

            // regra (1) e (2) - FIRST de terminal é ele mesmo
            if (!token.isVariavel) {
                resultado = incluirValor(resultado, token.texto);
                break;
            } else {
                if (token.texto == variavel) {
                    debugger;
                    break;
                }

                let primeiros = getConjuntoFirst(token.texto, gramatica);

                // regra (4) - conjunto FIRST deriva em Ɛ
                if (primeiros.join('').endsWith('%')) {
                    if (i != gramatica.indices[parte].tokens.length - 1) {
                        primeiros.splice(primeiros.length - 1, 1);
                    }
                    resultado = incluirValores(resultado, primeiros);
                    break;
                } else {
                    // regra (3) adiciona ao FIRST o FIRST do token atual que é variável
                    resultado = incluirValores(resultado, primeiros);
                    break;
                }
            }

        }
    });

    return resultado;
}