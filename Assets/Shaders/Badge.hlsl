void SDFPolygon_float(float2 p, float r, int n, float m, out float Color){
    float an = 3.141593/ float(n);
    float en = 3.141593/m;
    float2 acs = {cos(an),sin(an)};
    float2  ecs = {cos(en),sin(en)}; // ecs=vec2(0,1) and simplify, for regular polygon,

    // symmetry (optional)
    p.x = abs(p.x);

    // reduce to first sector
    float bn = (atan2(p.x,p.y) % (2.0*an)) - an;
    p = length(p) * float2(cos(bn), abs(sin(bn)));

    // line sdf
    p -= r*acs;
    p += ecs*clamp( -dot(p,ecs), 0.0, r*acs.y/ecs.y);
    Color = length(p)*sign(p.x);
}