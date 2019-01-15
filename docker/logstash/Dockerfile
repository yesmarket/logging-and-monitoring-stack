# FROM python:alpine3.8 AS build

# ARG DEVOPS_BASEURL
# ARG DEVOPS_USERNAME
# ARG DEVOPS_PAT
# ARG PCI_INSTANCE
# ARG _HTTP_PROXY

# RUN if [ ! -z "${_HTTP_PROXY}" -a "${_HTTP_PROXY}" != " " ]; then export http_proxy="${_HTTP_PROXY}"; fi
# RUN pip install requests pyyaml
# COPY ./setup.py /setup.py
# RUN python /setup.py ${DEVOPS_BASEURL} ${DEVOPS_USERNAME} ${DEVOPS_PAT}

FROM docker.elastic.co/logstash/logstash-oss:6.3.2 AS runtime

COPY ./logstash-codec-prometheus-0.5.0.gem /tmp
COPY ./logstash-output-applicationinsights-0.1.0.gem /tmp

RUN logstash-plugin install logstash-input-http_poller
#RUN logstash-plugin install --version '0.4.0' logstash-codec-prometheus
RUN logstash-plugin install /tmp/logstash-codec-prometheus-0.5.0.gem
RUN logstash-plugin install logstash-filter-drop
RUN logstash-plugin install logstash-filter-dissect
RUN logstash-plugin install logstash-filter-prune
RUN logstash-plugin install --version '0.2.0' logstash-output-loganalytics
RUN logstash-plugin install /tmp/logstash-output-applicationinsights-0.1.0.gem
RUN logstash-plugin install logstash-codec-json

COPY ./config/logstash.yml /usr/share/logstash/config
#COPY --from=build /usr/share/logstash/config/pipelines.yml /usr/share/logstash/config
COPY ./config/pipelines.yml /usr/share/logstash/config
#COPY --from=build /usr/share/logstash/pipeline /usr/share/logstash/pipeline
COPY ./pipeline/generic.conf /usr/share/logstash/pipeline/generic.conf
