#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da pilha
typedef struct No {
    int dado;
    struct No* proximo;
} No;

// Estrutura da pilha
typedef struct Pilha {
    No* topo;
} Pilha;

// Inicializa uma nova pilha
Pilha* criarPilha() {
    Pilha* p = (Pilha*) malloc(sizeof(Pilha));
    p->topo = NULL;
    return p;
}

// Empilha elemento no topo
void empilhar(Pilha* p, int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    novo->proximo = p->topo;
    p->topo = novo;
}

// Desempilha elemento do topo
int desempilhar(Pilha* p) {
    if (p->topo == NULL) return -1; // Pilha vazia
    No* temp = p->topo;
    int dado = temp->dado;
    p->topo = temp->proximo;
    free(temp);
    return dado;
}

// Imprime os elementos da pilha
void imprimirPilha(Pilha* p) {
    No* atual = p->topo;
    while (atual != NULL) {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    }
    printf("\n");
}

int main() {
    Pilha* p = criarPilha();
    // Coloca elementos na pilha
    empilhar(p, 5); empilhar(p, 10); empilhar(p, 15);
    imprimirPilha(p);
    // Remove elemento do topo
    desempilhar(p);
    imprimirPilha(p);
    return 0;
}

