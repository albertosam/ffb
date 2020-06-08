import { Gramatica } from '../modelos/garmatica';
import { ehTerminal } from './gramatica';
import { ValidadorLogger, Log } from './logger';


class Validador {
    cadeia: string[];
    ponteiro: number = - 1;
    pilha: string[];
    logger: ValidadorLogger;
    logAtual: Log;
    erro: boolean = false;

    constructor(private entrada: string, public gramatica: Gramatica) {
        this.cadeia = entrada.concat(' $ ').split(' ').map(a => a.trim()).filter(a => a.length > 0);
        this.pilha = [];

        this.pilha.push('$');
        this.pilha.push(gramatica.variaveis[0]);
        this.logger = new ValidadorLogger();
    }

    addPilha(token: string) {
        this.pilha.push(token);
    }

    popPilha() {
        this.pilha.pop();
    }

    addTokensNaPilha(celula: number) {
        let indice = this.gramatica.indices[celula];
        var novosTokens = indice.tokens;

        this.logAtual.mensagem = indice.variavel.concat(' -> ').concat(indice.texto);

        if (novosTokens[0].texto != '%') {
            for (let index = novosTokens.length - 1; index >= 0; index--) {
                let item = novosTokens[index].texto;
                this.addPilha(item);
            }
        }
    }

    buscarToken(): string {
        if (this.ponteiro > this.cadeia.length)
            return undefined;

        this.ponteiro++;
        let token = this.cadeia[this.ponteiro];
        return token;
    }

    buscarTopo() {
        let pos = this.pilha.length - 1;
        return this.pilha[pos];
    }

    pilhaVazia() {
        return this.pilha.length == 0;
    }

    addSucesso() {
        this.logAtual.mensagem = 'Entrada validada com sucesso!';
        this.logAtual.sucesso = true;
    }

    addErro(codigoErro: string, topo: string, token: string) {
        let first: string[] = [];
        let mensagem = '';
        this.erro = true;

        switch (codigoErro) {
            case 'FIM_INESPERADO':
                mensagem = `O token ${topo} não era esperado.`;
                break;

            case 'TOKEN_INVALIDO':
                let first = this.gramatica.regras[topo].first;
                if (!first)
                    throw 'Não encontrado FIRST para o token ' + first;

                first.forEach(function (item) {
                    if (item == '%') {
                        return;
                    }
                    first.push(item);
                });
                mensagem = `O token ${token} é inválido. Os seguintes tokens eram esperados: ${first.join(' ou ')}`;
                break;

            default:
                break;
        }

        let passo = this.logger.addErro(this.ponteiro, topo, token, this.cadeia.filter((a, c, arrar) => c > this.ponteiro).join(''), this.pilha, mensagem);
        return passo;
    }

    addLog(topo: string, token: string) {
        this.logAtual = this.logger.addLog(this.ponteiro, topo, token, this.cadeia.filter((a, c, arrar) => c > this.ponteiro).join(''), this.pilha);
    }
}

export function validar(entrada: string, gramatica: Gramatica) {
    let estouro = 1000;
    let i = 0;
    let topo: string = '';

    let validador = new Validador(entrada, gramatica);
    let tokenAtual = validador.buscarToken();

    while (true && i < estouro) {
        // armazena primeiro item da pilha
        topo = validador.buscarTopo();
        // entrada em log
        validador.addLog(topo, tokenAtual);

        if (ehTerminal(gramatica, topo)) {
            if (topo == tokenAtual) {
                if (topo == '$') {
                    break;
                }

                validador.popPilha();
                tokenAtual = validador.buscarToken();

                continue;
            } else {
                validador.popPilha();
                validador.addErro('FIM_INESPERADO', topo, tokenAtual);
                break;
            }
        } else {
            var celula = gramatica.tabela[topo][tokenAtual];
            if (!celula) {
                validador.addErro('TOKEN_INVALIDO', topo, tokenAtual);
                break;
            }

            validador.popPilha();
            validador.addTokensNaPilha(celula);
        }

        if (validador.pilhaVazia())
            break;

        i++;
    }

    if (!validador.erro)
        validador.addSucesso();

    return validador.logger;
}