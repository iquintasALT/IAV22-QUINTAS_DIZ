# IAV22-QUINTAS_DIZ
Trabajo final para la asignatura de IAV de la UCM.
## Propuesta
Mi propuesta es una de los ejemplos en la sección **[Adecuada/Excelente]**. Se trata de un simulador de coches y peatones navegando por una ciudad. Planeo utilizar las siguientes herramientas/algoritmos, aunque estos pueden cambiar a lo largo del desarrollo:

 - Generador de ciudades por celdas con algoritmo A*. Las ciudades serán configurables, permitiendo edificar carreteras y edificios principalmente como el usuario prefiera.
 - Coches siguen sus carriles siguiendo un algoritmo de búsqueda de caminos por puntos. Dependiendo de que me resulte más adecuado, usaré un NavMesh de Unity o implementaré una serie de marcadores que los coches podrán seguir para llegar a sus destinos.
 -  Distintos elementos de trafico: rotondas e intersecciones, con posibles señales, semáforos... Posibles implementaciones; aparcar coches de forma organizada... Todo esto se hará con la herramienta Behaviour Designer.
   - Agentes peatones que cruzan pasos de cebra y navegarán de forma similar a los coches, pero por la acera.
- Posibles añadidos:
	- Generación de mapas aleatorios, tal vez utilizando alguna técnica como ruido de Perlin 


Al ser algo muy modular, cualquier añadido que se me ocurra sobre nuevas tareas para los agentes puede ser implementado fácilmente en un posterior juego, similar a títulos con navegación automática de coches como por ejemplo Simcity o Rollecoaster Tycoon. Si el resultado final se pudiese aplicar para desarrollar un juego entraría dentro de la categoría **[Excelente]**.
