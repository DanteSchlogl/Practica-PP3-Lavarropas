# Preguntas y respuestas

## Sobre el ejercicio

**1. ¿Qué hace el programa?**
Simula un lavarropas que ejecuta un programa de lavado en cuatro etapas: llenar, lavar, enjuagar y centrifugar. Cada etapa tarda lo que dice el programa, y todo queda anotado en un archivo.

**2. ¿Dónde está el delay?**
En `Thread.Sleep(etapa.Segundos * 1000)`, adentro del `foreach` de `ServicioLavado.Lavar()`. Pausa el hilo actual los segundos que dura la etapa, sin gastar CPU.

**3. ¿Eso es usar hilos?**
No. `Thread.Sleep` no crea un hilo: duerme al que ya esta corriendo. El programa tiene un solo hilo, el principal, y las etapas van una atras de la otra.

**4. ¿Por qué no hace falta usar hilos acá?**
Porque las etapas son secuenciales: no podes centrifugar antes de enjuagar. Un hilo extra no adelantaria nada, solo complicaria el codigo.

**5. ¿Qué diferencia hay entre `Thread.Sleep` y `Task.Delay`?**
`Thread.Sleep` bloquea el hilo. `Task.Delay` espera sin bloquear, pero necesita `async`/`await`. Acá, como no hay una interfaz que tenga que seguir respondiendo, `Sleep` alcanza y es mas simple.

## Sobre las capas

**6. ¿Por qué está en cuatro proyectos?**
Para separar responsabilidades: el dominio no sabe nada de archivos ni de consola, el DAL solo guarda, la BLL decide el orden del lavado y la UI solo muestra.

**7. ¿Quién conoce a quién?**
Domain no depende de nadie. DAL depende de Domain. BLL depende de Domain y DAL. UI depende de BLL y Domain. Nunca al revés.

**8. ¿Por qué la UI no crea el `RegistroLavadoArchivo`?**
Porque entonces la UI conocería el DAL y se rompería la separación. Para eso está `FabricaLavado` en la BLL: la UI pide un `ServicioLavado` y no sabe de dónde sale el registro.

**9. ¿Para qué sirve el evento `Aviso`?**
Para que la BLL avise el progreso sin escribir en consola. La BLL no sabe que existe una consola; la UI se suscribe y decide cómo mostrarlo.

## Sobre errores y datos

**10. ¿Qué pasa si una etapa dura 0 o menos?**
`AgregarEtapa` tira `LavadoException` antes de arrancar, así no se rompe a mitad del lavado.

**11. ¿Qué pasa si no hay etapas cargadas?**
`Lavar()` tira `LavadoException` con el mensaje de que no hay ninguna etapa cargada.

**12. ¿Y si el archivo no se puede escribir?**
`RegistroLavadoArchivo` atrapa la excepción de .NET y la envuelve en `LavadoException`, así la UI recibe un error del dominio y no un `IOException`.

**13. ¿Por qué un archivo por día?**
El nombre lleva la fecha (`lavado_2026-10-31.txt`), así que la rotación sale sola: cuando cambia el día, escribe en otro archivo.

**14. ¿Dónde se guarda el registro?**
En la carpeta `Registros`, al lado de donde se ejecuta el programa. La ruta se pasa por parámetro a la fábrica, no está escrita adentro del DAL.

## Si te preguntan algo más difícil

**15. ¿Qué habría que cambiar para poder cancelar el lavado a mitad de camino?**
Pasarle un `CancellationToken` al servicio y cortar el `Sleep` en tramos cortos, revisando entre uno y otro si le pidieron cancelar. Con un solo `Sleep` largo no hay forma de interrumpirlo.

**16. ¿Y si quisieras dos lavarropas al mismo tiempo?**
Ahí sí habría dos hilos (o dos `Task`), uno por lavarropas. Cada uno con su propio objeto `Lavarropas` y su propio registro, así no comparten nada y no hace falta sincronizar.

**17. ¿Qué es una condición de carrera y puede pasar acá?**
Es cuando dos hilos tocan el mismo dato y el resultado depende de quién llegó primero. Acá no puede pasar, porque hay un solo hilo.

**18. ¿Por qué el `Lavarropas` guarda `EtapaActual` como texto y no como enum?**
Porque las etapas las carga quien usa el servicio y no son una lista fija. Si fueran siempre las mismas cuatro, un enum sería más prolijo.
