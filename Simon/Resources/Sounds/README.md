# Sons pour le jeu Simon

Placez les fichiers sons suivants dans ce dossier :

- intro_sound.wav : Son d'introduction
- red_sound.wav : Son pour le bouton rouge
- green_sound.wav : Son pour le bouton vert
- blue_sound.wav : Son pour le bouton bleu
- yellow_sound.wav : Son pour le bouton jaune
- success_sound.wav : Son de réussite
- gameover_sound.wav : Son de fin de partie
- button_sound.wav : Son pour les boutons d'interface

Vous pouvez trouver des sons gratuits sur des sites comme:
- freesound.org
- mixkit.co
- zapsplat.com
  \`\`\`

## 3. Mise à jour du fichier .csproj

Ajoutez les lignes suivantes à votre fichier .csproj pour inclure les sons comme ressources embarquées :

```xml project="Simon" file="Simon.csproj.addition" type="code"
&lt;!-- Ajoutez ces lignes à votre fichier .csproj -->
<ItemGroup>
  <EmbeddedResource Include="Resources\Sounds\*.wav" />
</ItemGroup>