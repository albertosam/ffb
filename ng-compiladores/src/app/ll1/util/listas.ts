


export function incluirValor(array: string[], valor: string) {
    if (!array.includes(valor))
        array = array.concat(valor);

    return array;
}


export function incluirValores(array: string[], valores: string[]) {
    valores.forEach(valor => {
        if (!array.includes(valor))
            array = array.concat(valor);
    });

    return array;
}