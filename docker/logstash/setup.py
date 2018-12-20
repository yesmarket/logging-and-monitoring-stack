import sys
import requests
import json
import os
import yaml
devops_baseurl = sys.argv[1]
devops_username = sys.argv[2]
devops_pat = sys.argv[3]
r1 = requests.get(f"{devops_baseurl}?api-version=4.1-preview.1", auth=(devops_username, devops_pat))
pipelines = []
for repo in r1.json()["value"]:
   id=repo["id"]
   path="/logstash.conf"
   r2 = requests.get(f"{devops_baseurl}/{id}/items?path={path}&api-version=4.1", auth=(devops_username, devops_pat))
   if r2.status_code == 200:
      name = repo["name"].split(".",1)[1].lower()
      fileName = f"{name}.conf"
      filePath = f"/usr/share/logstash/pipeline/{fileName}"
      pipeline = { "pipeline.id":name, "path.config":filePath }
      pipelines.append(pipeline)
      if not os.path.exists(os.path.dirname(filePath)):
         os.makedirs(os.path.dirname(filePath))
      text_file = open(filePath, "wb")
      text_file.write(r2.content)
      text_file.close()
pipelinesFilePath = "/usr/share/logstash/config/pipelines.yml"
if not os.path.exists(os.path.dirname(pipelinesFilePath)):
   os.makedirs(os.path.dirname(pipelinesFilePath))
with open("/usr/share/logstash/config/pipelines.yml", "w") as outfile:
    yaml.dump(pipelines, outfile, default_flow_style=False)
