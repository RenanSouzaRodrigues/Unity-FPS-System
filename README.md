# **Unity FPS System**

Esse sistema pode ser usado para iniciar o desenvolvimento de seus jogos FPS usando unity.
O propósito é simples, ter uma ajuda para sua prototipação.

## **Antes de tudo...**

É importante entender que este não um template para um projeto, tão pouco um projeto que possa ser simplesmente importado.
Tudo que está contido no sistema deve ser tratado como um componente do seu projeto.

Esse sistema está sendo desenvolvido na versão do Unity 2019.4.17f1, e é compatível com todas as versões do Unity a partir
da versão mencionada, caso presencie algum tipo de bug ou problema, por favor, criar um Issue nesse repositório.

### **Importate!**

> Este sistema não foi construindo usando o URP (Universal render pipeline), por isso, caso use este render pipeline, certifique-se de desabilitar os efeitos de Pós processamento presentes no sistema.

## **Instalando o Sistema** 

Para instalar o sistema, basta selecionar o arquivo da ultima versão na seção de <a href="#">releases</a>, 
baixar o arquivo e importar para dentro do seu projeto.

Você também pode escolher versões especificas mas isso não é recomendado visto que o sistema pode apresentar 
bugs em versões mais antigas. Opte sempre por versões estáveis caso for usa-lo em seu projeto.

Recomendo criar uma pasta dedicada e verificar se podem haver conflitos com outros scripts.
Mesmo que a nomenclatura das classes seja inteiramente contextual, classes como 'GameManager' estão presentes no sistema.

## **Como funciona?**

O sistema conta com um prefab que pode ser usado para iniciar seu projeto. 
O prefab em questão é o GameObject que representa o Player, ja possuindo as funcionalidades necessárias. Caso queira simplesmente implementar os scripts em um GameObject ou outro Prefab de sua escolha, certifique-se de que seu GameObject ou Prefab possua a seguinte estrutura:

|-> PlayerController (Empty Game Object)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----> Body<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----> Face (Opicional)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----> Camera<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----> GroundCheck<br>

O sistema também conta com algumas classes que podem ser utilizadas como auxiliares para realizar a modularização do seu controle FPS.

### **Importante!**
> Alguns Scriptable Objects são obrigatórios para o funcionamento correto do controlador FPS. A Seção **Scriptable Objects** descreve melhor quais classes são obrigatórias e quais são opcionais.

## **Classes MonoBehaviour**

Antes de mais nada é importante definir que todos os scripts seguem alguns padrões de qualidade para manter sua integridade. Caso opte por entender melhor o script, invariavelmente você irá se deparar com a palavra reservada _**this**_ que por padrão não é muito utilizada. 
Além disso é preciso se atentar para o fato de que as classes são distribuidas dentro de _**namespaces**_ que refletem diretamente a estrutura das pastas, como por exemplo a classe **FPSMovementController**, que se encontra dentro de **FPS\Player\FPSMovementController**

```cs
namepace FPS.Player {
    class FPSMovementController : MonoBehaviour { ... } 
}
```

> #### **FPS\Common\GameManager.cs**
> Esta classe é responsável pelo gerenciamento de estados e eventos globais e locais que podem acontecer durante o fluxo do seu jogo.
> Ela é responsável pelo controle das UI's, como por exemplo, o HUD, o painel de informações, o painel de controle de vida, o painel de controle de munição, etc.
Esta classe também controla a transição de cenas e audios globais.

> #### **FPS\Common\GlobalSoundManager.cs**
>Está é uma das classes auxiliares que o GameManager utiliza para controlar os audios globais.

> #### **FPS\Common\PersonalSoundManager.cs**
>Esta classe é uma classe auxiliar opicional que controla sons locais ou sons que são emitidos de maneira não locais por uma entidade ou um game object.

> #### **FPS\Player\FPSAimController.cs**
>Esta classe é responsável por controlar a camera do jogador, assim como a rotação do player. Pode ser completamente parametrizada pelo Scriptable Object ```AimProperties```

> #### **FPS\Player\FPSAimController.cs**
>Esta classe controla o movimento do player, Assim como as habilidades motoras que o player é capaz de realizar. Pode ser completamente parametrizada pelos Scriptable Objects `MovementProperties` e `CommonAttributes`

## **Scriptable Objects**
> #### **FPS\ScriptableObjects\CommonAttributes.cs**
> Classe que define as propriedades básicas de qualquer entidade que possa ser qualificada como algo que possa receber dano. Ou seja, assim como o Player é um entidade que pode receber dano, o inimigo também é uma entidade que pode receber dado. Portanto, esta classe é obrigatória para o funcionamento correto dos controles FPS.

> #### **FPS\ScriptableObjects\GameConfig.cs**
> Classe responsável por definir as configurações globais do jogo, como volumes globais e individuais.
