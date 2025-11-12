#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da árvore
typedef struct NoArvore {
    int dado;
    struct NoArvore* esq;
    struct NoArvore* dir;
} NoArvore;

// Insere elemento na árvore (Create)
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

// Imprime em ordem crescente (Read)
void emOrdem(NoArvore* raiz) {
    if (raiz != NULL) {
        emOrdem(raiz->esq);
        printf("%d ", raiz->dado);
        emOrdem(raiz->dir);
    }
}

// Busca valor na árvore (Read)
NoArvore* buscarArvore(NoArvore* raiz, int dado) {
    if (raiz == NULL || raiz->dado == dado) return raiz;
    if (dado < raiz->dado) return buscarArvore(raiz->esq, dado);
    else return buscarArvore(raiz->dir, dado);
}

// Busca o menor valor (auxiliar para remoção)
NoArvore* minValorNo(NoArvore* no) {
    NoArvore* atual = no;
    while (atual && atual->esq != NULL)
        atual = atual->esq;
    return atual;
}

// Remove valor da árvore (Delete)
NoArvore* removerArvore(NoArvore* raiz, int dado) {
    if (raiz == NULL) return raiz;

    if (dado < raiz->dado)
        raiz->esq = removerArvore(raiz->esq, dado);
    else if (dado > raiz->dado)
        raiz->dir = removerArvore(raiz->dir, dado);
    else {
        if (raiz->esq == NULL) {
            NoArvore* temp = raiz->dir;
            free(raiz);
            return temp;
        } else if (raiz->dir == NULL) {
            NoArvore* temp = raiz->esq;
            free(raiz);
            return temp;
        }
        NoArvore* temp = minValorNo(raiz->dir);
        raiz->dado = temp->dado;
        raiz->dir = removerArvore(raiz->dir, temp->dado);
    }
    return raiz;
}

// Atualiza valor de um nó (Update)
int atualizarArvore(NoArvore* raiz, int antigo, int novo) {
    NoArvore* no = buscarArvore(raiz, antigo);
    if (no != NULL) {
        no->dado = novo;
        return 1;
    }
    return 0;
}

int main() {
    NoArvore* raiz = NULL;
    int op, valor, antigo, novo;

    do {
        printf("\n--- MENU CRUD ARVORE BINARIA ---\n");
        printf("1. Inserir valor\n");
        printf("2. Listar em ordem\n");
        printf("3. Buscar valor\n");
        printf("4. Atualizar valor\n");
        printf("5. Remover valor\n");
        printf("0. Sair\nEscolha: ");
        scanf("%d", &op);

        switch(op) {
            case 1:
                printf("Insira o valor: ");
                scanf("%d", &valor);
                raiz = inserir(raiz, valor);
                printf("Inserido!\n");
                break;
            case 2:
                printf("Árvore em ordem: ");
                emOrdem(raiz);
                printf("\n");
                break;
            case 3:
                printf("Valor para buscar: ");
                scanf("%d", &valor);
                if (buscarArvore(raiz, valor))
                    printf("Valor %d encontrado!\n", valor);
                else
                    printf("Valor %d NÃO encontrado.\n", valor);
                break;
            case 4:
                printf("Valor antigo: ");
                scanf("%d", &antigo);
                printf("Novo valor: ");
                scanf("%d", &novo);
                if (atualizarArvore(raiz, antigo, novo))
                    printf("Valor atualizado!\n");
                else
                    printf("Valor antigo NÃO encontrado.\n");
                break;
            case 5:
                printf("Valor para remover: ");
                scanf("%d", &valor);
                raiz = removerArvore(raiz, valor);
                printf("Removido!\n");
                break;
            case 0:
                printf("Encerrando...\n");
                break;
            default:
                printf("Opção inválida!\n");
        }
    } while (op != 0);

    return 0;
}

