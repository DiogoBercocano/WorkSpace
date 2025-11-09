#include <stdio.h>
#include <stdlib.h>

// Define o nó da lista
typedef struct No {
    int dado;
    struct No* proximo;
} No;

No* ultimo = NULL;

// Adiciona um elemento na lista circular
void adicionar(int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    if (ultimo == NULL) {
        // Quando a lista está vazia, o novo nó aponta para ele mesmo
        ultimo = novo;
        ultimo->proximo = ultimo;
    } else {
        // Insere novo nó após o último e atualiza o ponteiro 'ultimo'
        novo->proximo = ultimo->proximo;
        ultimo->proximo = novo;
        ultimo = novo;
    }
}

// Remove um elemento da lista circular
int remover(int dado) {
    if (ultimo == NULL) return 0; // Lista vazia
    No *atual = ultimo->proximo, *anterior = ultimo;
    do {
        if (atual->dado == dado) {
            if (atual == ultimo && atual == ultimo->proximo) {
                // Caso único nó
                free(atual);
                ultimo = NULL;
            } else {
                // Remove nó do meio ou do final
                anterior->proximo = atual->proximo;
                if (atual == ultimo) ultimo = anterior;
                free(atual);
            }
            return 1;
        }
        anterior = atual;
        atual = atual->proximo;
    } while (atual != ultimo->proximo);
    return 0;
}

// Busca um nó pelo valor
No* buscar(int dado) {
    if (ultimo == NULL) return NULL;
    No* atual = ultimo->proximo;
    do {
        if (atual->dado == dado) return atual;
        atual = atual->proximo;
    } while (atual != ultimo->proximo);
    return NULL;
}

// Atualiza um valor existente na lista
void atualizar(int dadoAntigo, int novoDado) {
    No* no = buscar(dadoAntigo);
    if (no != NULL) no->dado = novoDado;
}

// Imprime todos os elementos da lista
void imprimirLista() {
    if (ultimo == NULL) {
        printf("Lista vazia\n");
        return;
    }
    No* atual = ultimo->proximo;
    do {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    } while (atual != ultimo->proximo);
    printf("\n");
}

int main() {
    // Inserção de elementos
    adicionar(1); adicionar(2); adicionar(3);
    imprimirLista();
    // Atualização de um elemento
    atualizar(2, 4);
    imprimirLista();
    // Remoção de um elemento
    remover(1);
    imprimirLista();
    return 0;
}

