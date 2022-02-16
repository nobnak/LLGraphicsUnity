# Object-oriented GL tool for Unity

## Usage
```csharp
public Texture tex;
public List<List<Vector2>> routes;
protected GLMaterial mat;

private void OnEnable() {
  mat = new GLMaterial();
}
private void OnDisable() {
  mat.Dispose();
}
private void OnRenderObject() {
  var c = Camera.current;

  var prop = new GLProperty() {
    SrcBlend = BlendMode.One,
    DstBlend = BlendMode.Zero,

    ZWriteMode = false,
    ZTestMode = GLProperty.ZTestEnum.ALWAYS,
  };
  using (new GLMatrixScope()) {
    GL.LoadIdentity();

    var aspect = (float)c.pixelWidth / c.pixelHeight;
    var unit = 10f / c.orthographicSize;

    GL.LoadOrtho();
    using (mat.GetScope(new GLProperty(prop) { Color = Color.red })) {
      var scale = new Vector3(unit / aspect, unit, 1f);

      foreach (var r in routes) {

        r.DrawLineStrip();

        foreach (var v in r) {
          using (new GLModelViewScope(Matrix4x4.TRS(v, Quaternion.identity, scale)))
            GLTool.QuadOutlineVertices().DrawLineStrip();
        }
      }
    }

    GLTool.LoadSquareOrtho(aspect);
    var texRect = new Rect(0.05f, 0.05f, 0.2f * tex.Aspect(), 0.2f);
    using (new GLModelViewScope(Matrix4x4.TRS(texRect.center, Quaternion.identity, texRect.size)))
    using (mat.GetScope(new GLProperty(prop) { MainTex = tex })) {
      GLTool.DrawQuad();
    }
  }
}
```
