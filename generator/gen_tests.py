import yaml
import argparse
from pathlib import Path

TEST_FILE_TEMPLATE = '''// AUTO-GENERATED TESTS. DO NOT EDIT MANUALLY.
// Source: {spec_source}
using System;
using NUnit.Framework;
using Module.Core;

namespace Module.Tests
{{
    [TestFixture]
    public class {module_name}TestsGenerated
    {{
        private {module_name} _sut;
        [SetUp] public void SetUp() => _sut = new {module_name}();
{test_methods}
    }}
}}
'''

TEST_METHOD_TEMPLATE = '''        [Test]
        [Description("{case_desc}")]
        public void Test_{method_name}_{case_name}()
        {{
            // Pre: {pre}
{act_code}
{assert_code}
        }}
'''

def fmt(v):
    if v is None: return "null"
    if isinstance(v, str): return f'"{v}"'
    return str(v)

def generate(spec, config):
    blocks = []
    for m in spec["methods"]:
        for eq in m.get("equivalence_classes", []):
            inputs = ", ".join(fmt(x) for x in eq["inputs"])
            case = eq["case"].replace(" ","_").replace("(","").replace(")","")
            if eq["expected"] == "ArgumentException":
                act = f"            Assert.Throws<ArgumentException>(() => _sut.{m['name']}({inputs}));"
                ass = "            // Exception expected"
            else:
                act = f"            var result = _sut.{m['name']}({inputs});"
                exp = eq["expected"]
                if isinstance(exp,str) and exp.startswith("[") and exp.endswith("]"):
                    inner = exp[1:-1].strip()
                    ass = f"            Assert.AreEqual(new int[] {{ {inner} }}, result);"
                else:
                    ass = f"            Assert.AreEqual({fmt(exp)}, result);"
            blocks.append(TEST_METHOD_TEMPLATE.format(
                case_desc=eq["case"], method_name=m["name"], case_name=case,
                pre=m["pre"], act_code=act, assert_code=ass))
    content = TEST_FILE_TEMPLATE.format(
        spec_source=config.get("spec_path","N/A"),
        module_name=spec["module"],
        test_methods="".join(blocks))
    out = Path(config["output_dir"])
    out.mkdir(parents=True, exist_ok=True)
    (out / f"{spec['module']}Tests.Generated.cs").write_text(content, encoding="utf-8")
    print("[OK] Generated")

if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--config", default="config.yaml")
    a = p.parse_args()
    with open(a.config, encoding="utf-8") as f: cfg = yaml.safe_load(f)
    with open(cfg["spec_path"], encoding="utf-8") as f: sp = yaml.safe_load(f)
    generate(sp, cfg)
