#include <stdio.h>
#include <stdlib.h>

// Nó da lista
typedef struct No {
    int dado;
    struct No* proximo;
} No;

No* ultimo = NULL;

// Adiciona elemento (Create)
void adicionar(int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    if (ultimo == NULL) {
        novo->proximo = novo;
        ultimo = novo;
    } else {
        novo->proximo = ultimo->proximo;
        ultimo->proximo = novo;
        ultimo = novo;
    }
}

// Remove elemento pelo valor (Delete)
int remover(int dado) {
    if (ultimo == NULL) return 0;
    No *atual = ultimo->proximo, *anterior = ultimo;
    do {
        if (atual->dado == dado) {
            if (atual == ultimo && atual == ultimo->proximo) {
                free(atual);
                ultimo = NULL;
            } else {
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

// Busca nó pelo valor (Read)
No* buscar(int dado) {
    if (ultimo == NULL) return NULL;
    No* atual = ultimo->proximo;
    do {
        if (atual->dado == dado) return atual;
        atual = atual->proximo;
    } while (atual != ultimo->proximo);
    return NULL;
}

// Atualiza nó pelo valor (Update)
int atualizar(int dadoAntigo, int novoDado) {
    No* no = buscar(dadoAntigo);
    if (no != NULL) {
        no->dado = novoDado;
        return 1;
    }
    return 0;
}

// Imprime toda a lista
void imprimirLista() {
    if (ultimo == NULL) {
        printf("Lista vazia\n");
        return;
    }
    No* atual = ultimo->proximo;
    printf("Lista Circular: ");
    do {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    } while (atual != ultimo->proximo);
    printf("\n");
}

int main() {
    int op, valor, novo, removido, encontrou;
    do {
        printf("\n--- MENU CRUD LISTA CIRCULAR ---\n");
        printf("1. Inserir elemento\n");
        printf("2. Listar elementos\n");
        printf("3. Buscar elemento\n");
        printf("4. Atualizar elemento\n");
        printf("5. Remover elemento\n");
        printf("0. Sair\nEscolha: ");
        scanf("%d", &op);

        switch (op) {
            case 1:
                printf("Valor para inserir: ");
                scanf("%d", &valor);
                adicionar(valor);
                printf("Inserido!\n");
                break;
            case 2:
                imprimirLista();
                break;
            case 3:
                printf("Valor para buscar: ");
                scanf("%d", &valor);
                encontrou = (buscar(valor) != NULL);
                if (encontrou)
                    printf("Valor %d está na lista.\n", valor);
                else
                    printf("Valor %d NÃO está na lista.\n", valor);
                break;
            case 4:
                printf("Valor antigo: ");
                scanf("%d", &valor);
                printf("Novo valor: ");
                scanf("%d", &novo);
                if (atualizar(valor, novo))
                    printf("Valor atualizado!\n");
                else
                    printf("Valor antigo NÃO encontrado.\n");
                break;
            case 5:
                printf("Valor para remover: ");
                scanf("%d", &valor);
                removido = remover(valor);
                if (removido)
                    printf("Removido!\n");
                else
                    printf("Valor NÃO encontrado.\n");
                break;
            case 0:
                printf("Finalizando...\n");
                break;
            default:
                printf("Opção inválida!\n");
        }
    } while (op != 0);
    return 0;
}

