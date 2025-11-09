#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da árvore
typedef struct NoArvore {
    int dado;
    struct NoArvore* esq;
    struct NoArvore* dir;
} NoArvore;

// Insere elemento na árvore de forma ordenada
NoArvore* inserir(NoArvore* raiz, int dado) {
    if (raiz == NULL) {
        NoArvore* novo = (NoArvore*) malloc(sizeof(NoArvore));
        novo->dado = dado;
        novo->esq = novo->dir = NULL;
        return novo;
    }
    if (dado < raiz->dado)
        raiz->esq = inserir(raiz->esq, dado);
    else if (dado > raiz->dado)
        raiz->dir = inserir(raiz->dir, dado);
    return raiz;
}

// Percorre a árvore em ordem crescente e imprime os valores
void emOrdem(NoArvore* raiz) {
    if (raiz != NULL) {
        emOrdem(raiz->esq);
        printf("%d ", raiz->dado);
        emOrdem(raiz->dir);
    }
}

// Busca valor na árvore
NoArvore* buscarArvore(NoArvore* raiz, int dado) {
    if (raiz == NULL || raiz->dado == dado) return raiz;
    if (dado < raiz->dado) return buscarArvore(raiz->esq, dado);
    else return buscarArvore(raiz->dir, dado);
}

// Busca o menor valor na subárvore
NoArvore* minValorNo(NoArvore* no) {
    NoArvore* atual = no;
    while (atual && atual->esq != NULL)
        atual = atual->esq;
    return atual;
}

// Remove valor da árvore
NoArvore* removerArvore(NoArvore* raiz, int dado) {
    if (raiz == NULL) return raiz;

    if (dado < raiz->dado) raiz->esq = removerArvore(raiz->esq, dado);
    else if (dado > raiz->dado) raiz->dir = removerArvore(raiz->dir, dado);
    else {
        // Nó com apenas um filho ou nenhum
        if (raiz->esq == NULL) {
            NoArvore* temp = raiz->dir;
            free(raiz);
            return temp;
        } else if (raiz->dir == NULL) {
            NoArvore* temp = raiz->esq;
            free(raiz);
            return temp;
        }
        // Nó com dois filhos: pega sucessor em ordem
        NoArvore* temp = minValorNo(raiz->dir);
        raiz->dado = temp->dado;
        raiz->dir = removerArvore(raiz->dir, temp->dado);
    }
    return raiz;
}

int main() {
    NoArvore* raiz = NULL;
    // Insere elementos na árvore
    raiz = inserir(raiz, 20);
    raiz = inserir(raiz, 10);
    raiz = inserir(raiz, 30);
    emOrdem(raiz); // Imprime em ordem
    printf("\n");
    // Remove elemento da árvore
    raiz = removerArvore(raiz, 10);
    emOrdem(raiz);
    printf("\n");
    return 0;
}

