-- 1. Transferência bancária com segurança
-- Crie uma transação que transfira R$200 da conta de Ana para Bruno.
-- Dica: use BEGIN TRANSACTION, UPDATE, COMMIT e ROLLBACK.
-- Teste: simule erro no segundo UPDATE e veja o comportamento.
begin transaction

update Contas set Saldo = Saldo - 200
where Nome = 'Ana';


-- 2. Inserção múltipla com falha proposital
-- Insira duas novas contas em uma única transação.
-- Na segunda, force um erro (ex: nome muito grande).
-- Verifique se nenhuma das duas foi inserida.


-- 3. Saque com verificação
-- Faça uma transação que permita o saque apenas se o saldo for suficiente.
-- Dica: use IF + ROLLBACK TRANSACTION quando o saldo for menor que o valor desejado.


