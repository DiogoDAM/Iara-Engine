import os
import re

def substituir_palavra_em_arquivos(diretorio, extensao, palavra_antiga, palavra_nova):
    """
    Substitui uma palavra por outra em todos os arquivos com determinada extensão,
    incluindo subdiretórios.
    
    Args:
        diretorio (str): Caminho do diretório raiz
        extensao (str): Extensão dos arquivos a serem modificados (ex: '.cs', '.txt')
        palavra_antiga (str): Palavra a ser substituída
        palavra_nova (str): Nova palavra
    """
    # Compilar regex para melhor performance
    padrao = re.compile(re.escape(palavra_antiga), re.IGNORECASE)
    
    # Contador de substituições
    total_substituicoes = 0
    total_arquivos = 0
    
    for raiz, _, arquivos in os.walk(diretorio):
        for arquivo in arquivos:
            if arquivo.endswith(extensao):
                caminho_completo = os.path.join(raiz, arquivo)
                total_arquivos += 1
                
                try:
                    # Ler conteúdo do arquivo
                    with open(caminho_completo, 'r', encoding='utf-8') as f:
                        conteudo = f.read()
                    
                    # Fazer a substituição
                    novo_conteudo, num_substituicoes = padrao.subn(palavra_nova, conteudo)
                    
                    if num_substituicoes > 0:
                        # Escrever o novo conteúdo se houve alterações
                        with open(caminho_completo, 'w', encoding='utf-8') as f:
                            f.write(novo_conteudo)
                        
                        total_substituicoes += num_substituicoes
                        print(f"Substituído {num_substituicoes} ocorrência(s) em {caminho_completo}")
                
                except Exception as e:
                    print(f"Erro ao processar {caminho_completo}: {str(e)}")
    
    print(f"\nResumo:")
    print(f"- Arquivos processados: {total_arquivos}")
    print(f"- Total de substituições: {total_substituicoes}")

if __name__ == "__main__":
    # Configurações (modifique conforme necessário)
    DIRETORIO_BASE = input("Digite o caminho do diretório raiz: ").strip()
    EXTENSAO = input("Digite a extensão dos arquivos (ex: .cs, .txt): ").strip().lower()
    PALAVRA_ANTIGA = input("Digite a palavra a ser substituída: ")
    PALAVRA_NOVA = input("Digite a nova palavra: ")
    
    # Verificar se o diretório existe
    if not os.path.isdir(DIRETORIO_BASE):
        print(f"Erro: O diretório '{DIRETORIO_BASE}' não existe.")
    else:
        print("\nIniciando substituição...")
        substituir_palavra_em_arquivos(DIRETORIO_BASE, EXTENSAO, PALAVRA_ANTIGA, PALAVRA_NOVA)
