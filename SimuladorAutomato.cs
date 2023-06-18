using System;
using System.Collections.Generic;

namespace SimuladorAutomato{

    public class AutomatoSimulator{
        // Aqui vai o código do simulador de autômato

        public class Automato{
            public HashSet<string> Estados {get; set;}
            public HashSet<char> Alfabeto {get; set;}
            public Dictionary<Tuple<string, char>, HashSet<string>> Transicoes {get; set;}
            public string EstadoInicial {get; set;}
            public HashSet<string> EstadosFinais{get;set;}

            // Construtor
            public Automato(){
                Estados = new HashSet<string>();
                Alfabeto = new HashSet<char>();
                Transicoes = new Dictionary<Tuple<string, char>, HashSet<string>>();
                EstadoInicial = "";
                EstadosFinais = new HashSet<string>();
            }

            public void AdicionarAlfabeto(char simbolo){
                Alfabeto.Add(simbolo);
            }

            public void AdicionarEstado(string estado){
                Estados.Add(estado);
            }

            public void AdicionarTransicao(string origem, string destino, char simbolo){
                var chaveTransicao = new Tuple<string, char>(origem, simbolo);

                if (!Transicoes.ContainsKey(chaveTransicao)){
                    Transicoes[chaveTransicao] = new HashSet<string>();
                }
                Transicoes[chaveTransicao].Add(destino);
            }

            public void DefinirEstadoInicial(string estado){
                EstadoInicial = estado;
            }
            
            public void DefinirEstadoFinal(string estadoFinal){
                EstadosFinais.Add(estadoFinal);
            }

            public bool VerificarSequencia(string sequencia){
                var estadosAtuais = new HashSet<string> { EstadoInicial };

                foreach (char simbolo in sequencia){
                    var proximosEstados = new HashSet<string>();
                    foreach (string estadoAtual in estadosAtuais){
                        var chaveTransicao = new Tuple<string, char>(estadoAtual, simbolo);

                        if (Transicoes.ContainsKey(chaveTransicao)){
                            proximosEstados.UnionWith(Transicoes[chaveTransicao]);
                        }
                    }

                    estadosAtuais = proximosEstados;
                }

                return estadosAtuais.Overlaps(EstadosFinais);
            }
        }


        static void Main(string[] args){
            // Código para testar o simulador de autômato
            Automato automato = new Automato();

            automato.Estados.Add("q0");
            automato.Estados.Add("q1");
            automato.Estados.Add("q2");

            automato.Alfabeto.Add('0');
            automato.Alfabeto.Add('1');

            automato.EstadoInicial = "q0";
            automato.DefinirEstadoFinal("q2");

            automato.AdicionarTransicao("q0", "q1", '0');
            automato.AdicionarTransicao("q0", "q1", '1');
            automato.AdicionarTransicao("q1", "q0", '0');
            automato.AdicionarTransicao("q1", "q2", '1');
            automato.AdicionarTransicao("q1", "q1", '1');
            automato.AdicionarTransicao("q2", "q0", '0');
            automato.AdicionarTransicao("q2", "q2", '1');

            // Console.WriteLine($"Estado inicial: '{automato.EstadoInicial}'");
            // Console.WriteLine("Estados finais: ");
            // foreach(string est in automato.EstadosFinais){
            //     Console.WriteLine(est);
            // }

            // Console.WriteLine("Estados: ");

            // foreach(string est in automato.Estados){
            //     Console.WriteLine(est);
            // }
            string sequencia1 = "";
            Console.WriteLine("Digite a sequencia: ");
            sequencia1 = Console.ReadLine();

            bool sequencia1Aceita = automato.VerificarSequencia(sequencia1);
            Console.WriteLine($"A sequência '{sequencia1}' é {(sequencia1Aceita ? "aceita" : "rejeitada")} pelo autômato.");

        }
    }
}

