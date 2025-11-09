#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da fila
typedef struct No {
    int dado;
    struct No* proximo;
} No;

// Estrutura da fila com ponteiro para inicio e fim
typedef struct Fila {
    No *inicio, *fim;
} Fila;

// Inicializa uma nova fila
Fila* criarFila() {
    Fila* f = (Fila*) malloc(sizeof(Fila));
    f->inicio = f->fim = NULL;
    return f;
}

// Insere elemento no final da fila
void enfileirar(Fila* f, int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    novo->proximo = NULL;
    if (f->fim == NULL) {
        // Primeira inserção, fila estava vazia
        f->inicio = f->fim = novo;
        return;
    }
    // Atualiza ponteiros para inserir no fim
    f->fim->proximo = novo;
    f->fim = novo;
}

// Remove elemento do início da fila
int desenfileirar(Fila* f) {
    if (f->inicio == NULL) return -1; // Fila vazia
    No* temp = f->inicio;
    int dado = temp->dado;
    f->inicio = f->inicio->proximo;
    if (f->inicio == NULL) f->fim = NULL;
    free(temp);
    return dado;
}

// Imprime os elementos da fila
void imprimirFila(Fila* f) {
    No* atual = f->inicio;
    while (atual != NULL) {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    }
    printf("\n");
}

int main() {
    Fila* f = criarFila();
    // Insere elementos na fila
    enfileirar(f, 10); enfileirar(f, 20); enfileirar(f, 30);
    imprimirFila(f);
    // Remove elemento do início
    desenfileirar(f);
    imprimirFila(f);
    return 0;
}

